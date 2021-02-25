export function preloadStylesheetResource(stylesheetUrl) {
    const el = document.createElement("LINK");
    el.setAttribute("href", stylesheetUrl);
    el.setAttribute("as", "stylesheet");
    el.setAttribute("rel", "preload");

    return 
        new Promise((resolve, reject) => {
            el.onload = () => resolve(el);
            el.onerror = (x) => reject(x);
            document.head.appendChild(el);
        })
        .catch(console.error)
        .finally(Element.prototype.remove);
}