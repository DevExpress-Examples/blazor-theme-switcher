"use strict";

export const ThemeController = (function () {
    let abortController;

    function getLink(attr) {
        return document.querySelector(`#switcher-container link[${attr}]`);
    }

    function getLinks(attr) {
        return [...document.querySelectorAll(`#switcher-container link[${attr}]`)];
    }

    function createLoadHandler(link, loadingPromises, signal, interception = false) {
        link.dxIntercepted = interception;
        let resolve;
        loadingPromises.push(new Promise(r => resolve = r));
        link.addEventListener('load', () => {
            if (signal.aborted && !link.dxIntercepted)
                link.remove();
            link.dxIntercepted = false;
            resolve();
        });
    }

    function updateTheme(url, group, loadingPromises, signal, target, isTargetBefore) {
        const attr = `${group}-theme-link`;
        const links = getLinks(attr);
        if (!links.length && !url || links.length === 1 && links[0].href === url) return [];

        if (url) {
            const link = links.find(l => l.href === url);

            if (link) {
                if (![...document.styleSheets].some(css => css.href === url)) {
                    createLoadHandler(link, loadingPromises, signal, true);
                }
            } else {
                const container = document.getElementById("switcher-container");
                const newLink = document.createElement("link");
                newLink.rel = "stylesheet";
                newLink.href = url;
                newLink.setAttribute(attr, "");
                createLoadHandler(newLink, loadingPromises, signal);
                if (target)
                    isTargetBefore ? target.before(newLink) : target.after(newLink);
                else
                    container.append(newLink);
            }
        }

        return links.filter(l => l.href !== url);
    }

    async function setStylesheetLinks(theme, bsUrl, bsThemeMode, dxUrl, hlUrl, reference) {
        abortController?.abort();
        abortController = new AbortController();
        const signal = abortController.signal;

        const loadingPromises = [];
        const Link = getLink('dx-link');
        const oldLinks = updateTheme(bsUrl, 'bs', loadingPromises, signal, Link, true)
            .concat(updateTheme(dxUrl, 'dx', loadingPromises, signal, getLink('bs-theme-link') ?? Link, !getLink('bs-theme-link')))
            .concat(updateTheme(hlUrl, 'hl', loadingPromises, signal));
        await Promise.all(loadingPromises);

        if (signal.aborted) return;

        for (const link of oldLinks) {
            link.remove();
        }

        document.querySelector("HTML").setAttribute("data-bs-theme", bsThemeMode);
        document.cookie = `ActiveTheme=${theme};path=/`;

        await reference.invokeMethodAsync("ThemeLoadedAsync");
    }

    return {
        setStylesheetLinks
    }
})();
