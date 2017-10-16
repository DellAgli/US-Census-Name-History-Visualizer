$(document).ready(() => {
    CallWebService("WebService.asmx", "GetStates", {}, function (data) {
        data.forEach((x) => {
            $("select[name='states[]']").append("<option value='"+x.State+"'>" + x.StateName + "</option>")
        })
        $("select[name='states[]']").bootstrapDualListbox();
    })

    $("#btnSearchNames").click(() => {
        if ($("#namesearch").val().length >= 2)
            CallWebService("WebService.asmx", "SearchNames", { strSearch: $("#namesearch").val(), intMethod: $("#chkMethod").prop("checked") ? 1 : 0 }, function (data) {
                $("#tblSearch tbody").empty()
                data.forEach((x) => {
                    $("#tblSearch tbody").append("<tr><td onclick='SelectName(this)'>" + x.Name + "</td></tr>")
                })
            })
    })

    $("#btnSubmit").click(() => {
        CallWebService("WebService.asmx", "GetGraphData", { strNamesArray: "|" + GetSelectedNames().join("|") + "|", strStatesArray: "|" + GetSelectedStates().join("|") + "|" }, function (data) {
            console.log(data)
            var chartAbs = bb.generate({
                "data": {
                    "x" : "Years",
                    "columns": [
                        ["Years"].concat(data.Years),
                        ["Males"].concat(data.Males),
                        ["Females"].concat(data.Females),
                    ],
                    "colors": {
                        "Males": "blue",
                        "Females": "red"
                    },

                },
                "bindto": "#divGraphAbs",
                "axis": {
                    "y": {
                        "min": 0
                    }
                }
                
            });

            var chartPer = bb.generate({
                "data": {
                    "x": "Years",
                    "columns": [
                        ["Years"].concat(data.YearsPercentage),
                        ["Percentage Male"].concat(data.MalesPercentage),
                    ],
                    "colors": {
                        "Males Percentage": "blue",
                    },
                    
                },
                "bindto": "#divGraphPer",
                "axis": {
                    "y": {
                        "max": 100,
                        "min": 0
                    }
                }
            });

        })
    })
})

function SelectName(eleTr) {
    var Name = $(eleTr).text()
    $("#tblSelect tbody").append("<tr><td onclick='RemoveName(this)'>" + Name + "</td></tr>")
    $(eleTr).parent().remove()
}
function RemoveName(eleTr) {
    $(eleTr).parent().remove()
}
function GetSelectedNames() {
    var arrayReturn = []
    $("#tblSelect tbody td").each((i,x) => {
        arrayReturn.push($(x).text())
    })
    return arrayReturn
}

function GetSelectedStates() {
    return $("select[name='states[]']").val();
}

