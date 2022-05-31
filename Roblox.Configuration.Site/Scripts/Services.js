$(function () {

    // Jquery selectors for client list page
    var clientListRevealClientKeyLinks = $(".reveal-client-key-link");
    var clientRevealClientKeySuccessClass = ".reveal-client-key-success";
    var clientListSearchClientKeyInput = $("#client-search-input");
    var clientListSearchClientKeyButton = $("#client-search-button");
    var clientListSearchClientKeyReset = $("#client-search-reset");
    var clientListTryAgainLink = $("#client-error-page-try-again");
    var clientErrorCaption = $("#client-error");
    var clientCaption = $("#client-caption");
    var clientDefaultNumber = $("#client-default-number");
    var clientRows = $(".client-row");

    function warningPrompt(promptToShow, callback) {
        var text = promptToShow + "<br/><br/>Type YES to confirm.";
        var message = text + `<input class="form-control" id="client-warning-input" name="client-warning-input" type="text" />`;
        bootbox.dialog({
            title: "WARNING",
            message: message,
            buttons: {
                cancel: {
                    label: "Cancel",
                    className: "btn-default"
                },
                confirm: {
                    label: "OK",
                    className: "btn-primary",
                    callback: function (result) {
                        if (result === null) {

                        } else if ($("#client-warning-input").val() == "YES") {
                            callback();
                        }
                    }
                }
            }
        });
    }

    function togglePrompt(title, name, value, callback) {
        bootbox.confirm({
            title: title,
            message: "Are you sure you want to set '<b>" + name + "</b>' to '<b>" + value + "</b>'?",
            buttons: {
                confirm: {
                    label: "Confirm"
                },
                cancel: {
                    label: "Cancel"
                }
            },
            callback: function (result) {
                if (result) {
                    callback();
                }
            }
        });
    }

    $("span[data-make-button]").button();

    ////////////// clients page starts //////////////
    var addClientDialog = $("#addClientDialog").dialog({
        title: "Create New Api Client",
        modal: true,
        resizable: false,
        width: 500,
        autoOpen: false,
        buttons: {
            "Generate Guid": function () {
                // get a guid from the server
                $.get("/ServicesConfig/GenerateGuid", function (guid) {
                    $("#clientKey").val(guid);
                });
            },

            "Save": function () {
                var params = {
                    key: $("#clientKey").val(),
                    note: $("#clientNote").val(),
                };

                $.post("/ServicesConfig/AddClient", params, function () {
                    addClientDialog.dialog("close");
                    window.location.reload();
                });
            },

            "Cancel": function () {
                addClientDialog.dialog("close");
            }
        }
    });

    $("#create-new-client-button").click(function () {
        addClientDialog.dialog("open");
    });

    function removeClient(id) {
        var params = {
            id: id
        };

        $.post("/ServicesConfig/RemoveClient", params, function () {
            window.location.reload();
        });
    }

    $(".remove-client-link").click(function () {
        var id = $(this).closest("tr").data("id");
        var client = $(this).closest("tr").data("note");

        warningPrompt("Are you sure you want to remove the client '<b>" + client + "</b>'?",
            function () { removeClient(id); }
        );
        return false;
    });

    // This generator comes from https://stackoverflow.com/questions/105034/create-guid-uuid-in-javascript/2117523#2117523
    function guid() {
        return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g,
            c => (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
        )
    }

    var duplicateClientDialog = $("#duplicateClientDialog").dialog({
        title: "Duplicate Api Client",
        modal: true,
        resizable: false,
        width: 500,
        autoOpen: false,
        buttons: {
            "Generate Guid": function () {
                $("#duplicateClientKey").val(guid());
            },

            "Save": function () {
                var params = {
                    id: parseInt($("#duplicateTemplateId").val()),
                    key: $("#duplicateClientKey").val(),
                    note: $("#duplicateClientNote").val()
                };

                $.post("/ServicesConfig/DuplicateClient", params, function () {
                    duplicateClientDialog.dialog("close");
                    window.location.reload();
                });
            },

            "Cancel": function () {
                duplicateClientDialog.dialog("close");
            }
        }
    });

    $(".duplicate-client-link").click(function () {
        var id = $(this).closest("tr").data("id");
        var name = $(this).closest("tr").data("note");

        $("#duplicateTemplateId").val(id);
        $("#duplicateTemplateName").html(name);
        $("#duplicateClientNote").val(name + " Copy");
        duplicateClientDialog.dialog("open");
    });

    // Click handle for the client list reveal key links
    clientListRevealClientKeyLinks.click(function (event) {
        event.preventDefault();
        var $component = $(this);

        // retrieve client id from clicked element
        var clientId = $component.data("id");
        var params = {
            clientId: clientId
        };

        // call api service to get client associated with id
        $.get("/ServicesConfig/RevealClientKey", params, function (clientKey) {
            if (clientKey) {
                // show result and remove click handler
                $component.closest(clientRevealClientKeySuccessClass).html(clientKey);
                $component.unbind("click");
            } else {
                // throw error
                $component.removeClass("success").removeClass("loading").addClass("error");
            }
        });
    });

    // searches client list by key when user clicks search button 
    clientListSearchClientKeyButton.on("click", function () {
        var clientKey = clientListSearchClientKeyInput.val();

        var params = {
            clientKey: clientKey.toLowerCase()
        };

        clientErrorCaption.hide();

        // checks if user has entered any values
        if (clientKey) {
            // call api service to get client associated with id
            $.get("/ServicesConfig/SearchApiClientIDListByKey", params, function (clientIDList) {

                // failure from elevated action
                if (clientIDList === false) {
                    clientErrorCaption.show("fast");
                } else {
                    // hide all of the rows
                    clientRows.hide();

                    // reveal rows with approved ids
                    for (var i = 0; i < clientIDList.length; i++) {
                        var clientID = clientIDList[i];

                        $("tr[data-id='" + clientID + "']").show();
                    }
                    clientCaption.text(clientIDList.length + " clients found.").show();
                    clientDefaultNumber.hide();
                }
            });
        } else {
            clientListResetSearch();
        }
    });

    // resets the client list and removes any search terms/values
    function clientListResetSearch() {
        clientCaption.text("").hide();
        clientDefaultNumber.show();
        clientRows.show();
        clientErrorCaption.hide();
        clientListSearchClientKeyInput.val("");
    }

    clientListSearchClientKeyReset.click(function () {
        clientListResetSearch();
    });

    // performs a refresh for the client on error page
    clientListTryAgainLink.click(function () {
        location.reload();
    });

    function toggleClient(id, isValid) {
        var params = {
            isValid: isValid,
            id: id
        };

        $.post("/ServicesConfig/ToggleClient", params, function () {
            window.location.reload();
        });
    }

    $("button[data-toggle-client]").click(function () {
        var isValid = $(this).data("toggle-value");
        var id = $(this).closest("tr").data("id");
        var note = $(this).closest("tr").data("note");

        togglePrompt("Toggle Client", note, isValid, function () { toggleClient(id, isValid); });
    });

    function renameClient(isValid, id, newNote) {
        // hack that retrieves the current API client key value in order to update
        $.get("/ServicesConfig/RevealClientKey", { clientId: id }, function (clientKey) {
            var params = {
                isValid: isValid,
                id: id,
                note: newNote,
                key: clientKey
            };

            $.post("/ServicesConfig/UpdateClient", params, function () {
                window.location.reload();
            });
        });
    }

    $("button.rename-client-link").click(function () {
        var tr = $(this).closest("tr");
        var isValid = tr.data("valid");
        var id = tr.data("id");
        var note = tr.data("note");

        bootbox.prompt({
            title: "Type in a new name for the client",
            callback: function (newNote) {
                if (!(newNote === null || newNote === '')) {
                    warningPrompt("Are you sure you want to rename <br/><b>" + note + "</b> -> <b>" + newNote + "</b>?",
                        function () { renameClient(isValid, id, newNote); }
                    );
                }
            }
        });
    });

    //////////////// clients page ends //////////////////////


    ////////////////// services page starts //////////////////////

    $("#create-new-service-button").click(function () {
        bootbox.dialog({
            title: "Create New Api Service",
            message: `<label for="serviceName">Name: </label><input class="form-control" id="serviceName" name="serviceName" type="text" />`,
            buttons: {
                cancel: {
                    label: "Cancel",
                    className: "btn-default"
                },
                confirm: {
                    label: "Save",
                    className: "btn-primary",
                    callback: function () {
                        var params = {
                            name: $("#serviceName").val(),
                        };

                        $.post("/ServicesConfig/AddService", params, function () {
                            window.location.reload();
                        });
                    }
                }
            }
        });
    });

    function toggleService(serviceId, enableService) {
        var params = {
            serviceId: serviceId,
            enableService: enableService
        };

        $.post("/ServicesConfig/ToggleService", params, function () {
            window.location.reload();
        });
    }

    $("button[data-toggle-service]").click(function () {
        var enableService = $(this).data("toggle-value");
        var serviceId = $(this).closest("tr").data("id");
        var serviceName = $(this).closest("tr").data("name");

        togglePrompt("Toggle Service", serviceName, enableService, function () { toggleService(serviceId, enableService); });
    });

    //////////////// services page ends ////////////////////////////

    ///////////// operations page starts ///////////////////
    $("#create-new-operation-button").click(function () {
        bootbox.dialog({
            title: "Create New Operation",
            message: `<label for="operationName">Name: </label><input class="form-control" id="operationName" name="operationName" type="text" />`,
            buttons: {
                cancel: {
                    label: "Cancel",
                    className: "btn-default"
                },
                confirm: {
                    label: "Save",
                    className: "btn-primary",
                    callback: function () {
                        var params = {
                            operationName: $("#operationName").val(),
                            serviceName: $("#current-service-name").val()
                        };

                        $.post("/ServicesConfig/AddOperation", params, function () {
                            window.location.reload();
                        });
                    }
                }
            }
        });
    });


    function deleteOperation(operationId) {
        // do deletion
        var params = {
            operationId: operationId
        };

        $.post("/ServicesConfig/DeleteOperation", params, function () {
            window.location.reload();
        });
    }

    $("a[data-delete-operation]").click(function () {
        var tr = $(this).closest("tr");
        var operationName = tr.data("name");
        var operationId = $(this).data("operationid");
        warningPrompt("Are you sure you want to delete operation '" + operationName + "'?",
            function () { deleteOperation(operationId); }
        );
        return false;
    });

    function toggleOperation(operationId, operationName, serviceName, enableOperation) {
        var params = {
            operationId: operationId,
            enableOperation: enableOperation
        };
        console.log(params);
        $.post("/ServicesConfig/ToggleOperation", params, function () {
            window.location.reload();
        });
    }

    $("button[data-toggle-operation]").click(function () {
        var enableOperation = $(this).data("toggle-value");
        var operationId = $(this).closest("tr").data("id");
        var serviceName = $("#current-service-name").val();
        var operationName = $(this).closest("tr").data("name");

        togglePrompt("Toggle Operation", operationName, enableOperation, function () { toggleOperation(operationId, operationName, serviceName, enableOperation); });
    });


    ///////////// operations page ends ///////////////////

    /////////// service authorizations page starts ////////////////
    var serviceAuthorizationDialog = $("#serviceAuthorizationDialog").dialog({
        title: "Create New Service Authorization",
        modal: true,
        resizable: false,
        width: 530,
        autoOpen: false,
        buttons: {
            "Save": function () {
                var params = {
                    key: $("#clientKey").val(),
                    serviceName: $("#serviceName").val(),
                    authorizationType: $("#authorizationType").val()
                };

                $.post("/ServicesConfig/AddServiceAuthorization", params, function () {
                    serviceAuthorizationDialog.dialog("close");
                    window.location.reload();
                });
            },

            "Cancel": function () {
                serviceAuthorizationDialog.dialog("close");
            }
        }
    });

    $("#create-new-service-authorization-button").click(function () {
        serviceAuthorizationDialog.dialog("open");
    });

    function addServiceAuthorization(key, serviceName, authorizationType) {
        var params = {
            key: key,
            serviceName: serviceName,
            authorizationType: authorizationType
        };

        $.post("/ServicesConfig/AddServiceAuthorization", params, function () {
            window.location.reload();
        });
    }

    $("button[data-change-service-authorization]").click(function () {
        var tr = $(this).closest("tr");
        var serviceName = tr.data("service-name");
        var authorizationType = tr.data("authorization-type");
        var key = $("#clientKey").val();
        warningPrompt("Are you sure you want to change authorization of '<b>" + serviceName + "</b>' to '<b>" + authorizationType + "</b>'? Please note that changing authorization to 'None' will delete it.",
            function () { addServiceAuthorization(key, serviceName, authorizationType); }
        );
        return false;
    });
    /////////// service authorizations page ends ////////////////

    /////////// operation authorizations page starts ////////////////
    var serviceNameDropDown = $("#operationAuthorizationDialog #serviceName");
    var operationNameDropDown = $("#operationAuthorizationDialog #operationName");

    var operationAuthorizationDialog = $("#operationAuthorizationDialog").dialog({
        title: "Create New Operation Authorization",
        modal: true,
        resizable: false,
        width: 530,
        autoOpen: false,

        buttons: {
            "Save": function () {
                var params = {
                    key: $("#clientKey").val(),
                    serviceName: serviceNameDropDown.val(),
                    operationName: operationNameDropDown.val(),
                    authorizationType: $("#authorizationType").val()
                };

                $.post("/ServicesConfig/AddOperationAuthorization", params, function () {
                    operationAuthorizationDialog.dialog("close");
                    window.location.reload();
                });
            },

            "Cancel": function () {
                operationAuthorizationDialog.dialog("close");
            }
        }
    });

    serviceNameDropDown.change(function () {
        $.get("/ServicesConfig/GetOperations", { serviceName: serviceNameDropDown.val() }, function (opsArray) {
            operationNameDropDown.empty(); // clear
            for (var i = 0; i < opsArray.length; i++) {
                var opName = opsArray[i];
                operationNameDropDown.append("<option>" + opName + "</option>");
            }
        });
    });

    var createNewOpAuthButton = $("#create-new-operation-authorization-button");
    createNewOpAuthButton.one("click", function () {
        serviceNameDropDown.trigger("change"); // pre-populate for the first time only
    });
    createNewOpAuthButton.click(function () {
        operationAuthorizationDialog.dialog("open");
    });


    function addOperationAuthorization(key, serviceName, operationName, authorizationType) {
        var params = {
            key: key,
            serviceName: serviceName,
            operationName: operationName,
            authorizationType: authorizationType
        };

        $.post("/ServicesConfig/AddOperationAuthorization", params, function () {
            window.location.reload();
        });
    }

    $("a[data-change-operation-authorization]").click(function () {
        var tr = $(this).closest("tr");
        var serviceName = tr.data("service-name");
        var operationName = tr.data("operation-name");
        var authorizationType = $(this).text();
        var key = $("#clientKey").val();
        warningPrompt("Are you sure you want to change authorization of operation '" + operationName + "' of service '" + serviceName + "' to '" + authorizationType + "'? Please note that changing authorization to 'None' will delete it.",
            function () { addOperationAuthorization(key, serviceName, operationName, authorizationType); }
        );
        return false;
    });
    /////////// operation authorizations page ends ////////////////
});


