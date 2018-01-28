$(document).ready(function () {
    
});


// for session storage in local web browser..
function sessionStorage(key, value, type) {
    try {
        if (typeof (localStorage) !== "undefined") {
            if (type) {
                if (key && value) {
                    if (type == "set") {
                        // for saving the value in the session storage
                        localStorage.setItem(key, JSON.stringify(value));
                        return true;
                    }
                    else {
                        console.log("key or value is undefined");
                        return null;
                    }
                }

                if (key) {
                    if (type == "get") {
                        // fetches the data based on the key
                        return JSON.parse(localStorage.getItem(key));
                    }
                    else if (type == "remove") {
                        // removes the value base on the key
                        localStorage.removeItem(key);
                        return true;
                    }
                    else if (type == "clear") {
                        // clear the session completely means clearing all the keys.
                        localStorage.clear();
                        return true;
                    }
                }
                else {
                    console.log("key is undefined");
                    return null;
                }
            }
            else {
                console.log("type is null");
                return null;
            }
        }
        else {
            console.log("session storage is undefined");
            return null;
        }
        return false;
    } catch (e) {
        console.log(e);
        alert("System error. Please retry after some time.");
    }
}

