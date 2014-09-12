Sys.loader.defineScripts(null, [
{
    name: "msEasing",
    releaseUrl: "/scripts/Plugins/jquery.easing.js",
    isLoaded: !!(window.msEasing)

},
{
    name: "msUICore",
    releaseUrl: "/scripts/jquery-ui-1.8.16.custom.min.js",
    isLoaded: !!(window.msUICore)


},
{
    name: "msModal",
    releaseUrl: "/scripts/Plugins/jquery.simplemodal-1.3.5.js",
    isLoaded: !!(window.msEasing && window.msModal),
    dependencies: ["msEasing", "msUICore"]

},
{
    name: "msWindow",
    releaseUrl: "/scripts/Controls/msWindow.js",
    isLoaded: !!(window.msModal && window.msWindow),
    dependencies: ["msModal"]

},
{
    name: "msMasked",
    releaseUrl: "/scripts/Plugins/jquery.maskedinput-1.2.2.min.js",
    isLoaded: !!(window.msMasked)


},
{
    name: "msWaterMark",
    releaseUrl: "/scripts/Plugins/jquery.watermarkinput.js",
    isLoaded: !!(window.msWaterMark)


},
{
    name: "msComboBox",
    releaseUrl: "/scripts/Controls/msComboBox.js",
    isLoaded: !!(window.msComboBox),
    dependencies: ["msEasing"]

},
{
    name: "msJSON",
    releaseUrl: "/scripts/Plugins/jquery.json.js",
    isLoaded: !!(window.msJSON)


},
{
    name: "jQueryHoverIntent",
    releaseUrl: "/scripts/Plugins/jquery.hoverIntent.js",
    isLoaded: !!(window.jQueryHoverIntent)


},
{
    name: "jQueryMetadata",
    releaseUrl: "/scripts/Plugins/jquery.metadata.js",
    isLoaded: !!(window.jQueryMetadata)


},
{
    name: "msMenu",
    releaseUrl: "/scripts/Controls/mbMenu.2.7.js",
    isLoaded: !!(window.msMenu),
    dependencies: ["jQueryHoverIntent", "jQueryMetadata"]

},
{
    name: "msMenuImages",
    releaseUrl: "/scripts/Controls/menu.images.js",
    isLoaded: !!(window.msMenuImages),
    dependencies: ["msMenu"]

},
{
    name: "msTabs",
    releaseUrl: "/scripts/Controls/tab.js",
    isLoaded: !!(window.msTabs)


},
{
    name: "msTabHistory",
    releaseUrl: "/scripts/Controls/tab.history.js",
    isLoaded: !!(window.msTabHistory),
    dependencies: ["msTabs"]

}
]);