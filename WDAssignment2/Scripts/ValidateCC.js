$(document).ready(function () {

    // Delay by 10ms for ID assignment to textboxes
    setTimeout(function () {

        // When paybutton clicked
        $("#PayButton").click(function () {

            // Regex to validate to
            var numberRegex = /^[0-9]{16}$/;
            var nameRegex = /^[a-zA-Z]{1}[\s]{1}[a-zA-Z]{1}$/;
            var expRegex = /^[0-9]{2}[/]{1}[0-9]{2}$/;
            var csvRegex = /^[0-9]{3}$/;

            // Take values from fields
            var number = $("#CCNumber").val();
            var name = $("#CCName").val();
            var expiry = $("#CCExpiry").val();
            var csv = $("#CSV").val();

            // Validate CC Number entered
            if (number == "") {
                alert("Enter Credit Card Number");
                return false;
            }
                // And conforms to 16 Digits
            else if (!numberRegex.test(number)) {
                alert("Invalid Credit Card Number. 16 Digits.")
                return false;
            }

            // Validate name entered
            if (name == "") {
                alert("Enter Credit Card Name");
                return false;
            }
                // And conforms to A A
            else if (!nameRegex.test(name)) {
                alert("Invalid Name. A A.")
                return false;
            }

            // Validate expiry entered
            if (expiry == "") {
                alert("Enter Credit Card Expiry");
                return false;
            }
                // And conforms to 01/01
            else if (!expRegex.test(expiry)) {
                alert("Invalid Credit Card Expiry. 01/01.")
                return false;
            }
                // And has not expired
            else {
                var split = expiry.split("/")

                if (split[1] < 14) {
                    alert("Credit Card Expired");
                    return false;
                }

                if (split[1] == 14)
                    if (split[0] < 8) {
                        alert("Credit Card Expired");
                        return false;
                    }
            }

            // Validate csv entered
            if (csv == "") {
                alert("Enter Credit Card CSV");
                return false;
            }
                // And conforms to 123
            else if (!csvRegex.test(csv)) {
                alert("Invalid Credit Card CSV. 123.")
                return false;
            }

            // Return true if valid
            return true;

        });
    });
});