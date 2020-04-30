window.functions = () => { };

window.functions.setTimeout = (time) => {
    setTimeout(function () {
        DotNet.invokeMethod('Musicio.Client.Web', 'ExecuteTimeoutFunc');
    }, time);
};