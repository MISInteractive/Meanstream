Sys.loader.defineScripts(null, [
{
    name: "msEasing",
    releaseUrl: "/scripts/Plugins/jquery.easing.js",
    isLoaded: !!(Window.msEasing)

},
{
    name: "msUICore",
    releaseUrl: "/scripts/jquery-ui-1.8.16.custom.min.js",
    isLoaded: !!(Window.msUICore)


},
{
    name: "msModal",
    releaseUrl: "/scripts/Plugins/jquery.simplemodal-1.3.5.js",
    isLoaded: !!(Window.msEasing && Window.msModal),
    dependencies: ["msEasing", "msUICore"]

},
{
    name: "msWindow",
    releaseUrl: "/scripts/Controls/msWindow.js",
    isLoaded: !!(Window.msModal && Window.msWindow),
    dependencies: ["msModal"]

},
{
    name: "msMasked",
    releaseUrl: "/scripts/Plugins/jquery.maskedinput-1.2.2.min.js",
    isLoaded: !!(Window.msMasked)


},
{
    name: "msWaterMark",
    releaseUrl: "/scripts/Plugins/jquery.watermarkinput.js",
    isLoaded: !!(Window.msWaterMark)


},
{
    name: "msComboBox",
    releaseUrl: "/scripts/Controls/msComboBox.js",
    isLoaded: !!(Window.msComboBox),
    dependencies: ["msEasing"]

},
{
    name: "msJSON",
    releaseUrl: "/scripts/Plugins/jquery.json.js",
    isLoaded: !!(Window.msJSON)


},
{
    name: "jQueryHoverIntent",
    releaseUrl: "/scripts/Plugins/jquery.hoverIntent.js",
    isLoaded: !!(Window.jQueryHoverIntent)


},
{
    name: "jQueryMetadata",
    releaseUrl: "/scripts/Plugins/jquery.metadata.js",
    isLoaded: !!(Window.jQueryMetadata)


},
{
    name: "msMenu",
    releaseUrl: "/scripts/Controls/mbMenu.2.7.js",
    isLoaded: !!(Window.msMenu),
    dependencies: ["jQueryHoverIntent", "jQueryMetadata"]

},
{
    name: "msMenuImages",
    releaseUrl: "/scripts/Controls/menu.images.js",
    isLoaded: !!(Window.msMenuImages),
    dependencies: ["msMenu"]

},
{
    name: "msTabs",
    releaseUrl: "/scripts/Controls/tab.js",
    isLoaded: !!(Window.msTabs)


},
{
    name: "msTabHistory",
    releaseUrl: "/scripts/Controls/tab.history.js",
    isLoaded: !!(Window.msTabHistory),
    dependencies: ["msTabs"]

}
]);