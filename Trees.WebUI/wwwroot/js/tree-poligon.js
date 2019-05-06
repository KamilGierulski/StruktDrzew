var TreePoligon = function() {
    this.Tree = null;
};

var TreePoligonService = (function () {

    var initChart = function (graphId) {
        var dataSets = {
            nodes: new vis.DataSet([]),
            edges: new vis.DataSet([])
        };

        $.ajax({
            type: "GET",
            url: "/api/graph/" + graphId,
            success: function (data) {
                if (data.success) {
                    dataSets.nodes = new vis.DataSet(data.data.nodes);
                    dataSets.edges = new vis.DataSet(data.data.edges);
                } else {
                    alert("Błąd podczas pobierania grafu!");
                }
            },
            failure: function (errMsg) {
                alert(errMsg);
            }
        }).done(function() {
            TreePoligon.Tree = null;

            function destroy() {
                if (TreePoligon.Tree !== null) {
                    TreePoligon.Tree.destroy();
                    TreePoligon.Tree = null;
                }
            }

            function toggleCollapse(nodeId, nodesToUpdate) {
                var childrenIds = TreePoligon.Tree.getConnectedNodes(nodeId, "to");
                if (childrenIds) {
                    childrenIds.forEach(function (element, index, array) {
                        var node = TreePoligon.Tree.body.data.nodes._data[element];
                        node.hidden = !node.hidden;

                        nodesToUpdate.push(node);

                        toggleCollapse(element, nodesToUpdate);
                    });
                }
            }

            function draw() {
                destroy();

                var locales = {
                    pl: {
                        edit: "Edytuj",
                        del: "Usuń zaznaczony",
                        back: "Cofnij",
                        addNode: "Dodaj węzeł",
                        addEdge: "Dodaj krawędź",
                        editNode: "Edytuj węzeł",
                        editEdge: "Edytuj krawędź",
                        addDescription: "Kliknij na pustej przestrzeni aby dodać węzeł.",
                        edgeDescription: "Kliknij na węzeł i przeciągnij linię aby powiązać go z innym węzłem.",
                        editEdgeDescription: "Kliknij na zakończeniu krawędzi i pociągnij, aby połączyć nią węzły.",
                        createEdgeError: "Nie można połączyć.",
                        deleteClusterError: "Nie można usunąć.",
                        editClusterError: "Nie można edytować."
                    }
                };

                // create a network
                var container = document.getElementById("tree");
                var options = {
                    locale: "pl",
                    locales: locales,
                    autoResize: true,
                    height: "500px",
                    width: "100%",
                    layout: {
                        hierarchical: {
                            enabled: true,
                            direction: "UD",
                            //parentCentralization: true,
                            sortMethod: "directed"
                        }
                    },
                    interaction: {
                        selectConnectedEdges: false,
                        //dragNodes: false
                    },
                    manipulation: {
                        enabled: true,
                        addNode: function (nodeData, callback) {
                            //$("addNodeModal").modal("show");

                            var nodeName = $("#name").val();
                            if (nodeName) {
                                nodeData.label = $("#name").val();
                                callback(nodeData);
                                $("#name").val("");
                            } else {
                                alert("Uzupełnij nazwę węzła");
                            }

                        }
                    },
                };
                TreePoligon.Tree = new vis.Network(container, dataSets, options);

                TreePoligon.Tree.on("click", function (e) {
                    var nodeId = e.nodes[0];

                    var nodesToUpdate = [];
                    toggleCollapse(nodeId, nodesToUpdate);
                    TreePoligon.Tree.body.data.nodes.update(nodesToUpdate);

                    TreePoligon.Tree.fit();
                });
            }

            draw();
        });
    };

    var initEmptyChart = function () {
        var dataSets = {
            nodes: new vis.DataSet([]),
            edges: new vis.DataSet([])
        };
        
        TreePoligon.Tree = null;

        function destroy() {
            if (TreePoligon.Tree !== null) {
                TreePoligon.Tree.destroy();
                TreePoligon.Tree = null;
            }
        }

        function draw() {
            destroy();

            var locales = {
                pl: {
                    edit: "Edytuj",
                    del: "Usuń zaznaczony",
                    back: "Cofnij",
                    addNode: "Dodaj węzeł",
                    addEdge: "Dodaj krawędź",
                    editNode: "Edytuj węzeł",
                    editEdge: "Edytuj krawędź",
                    addDescription: "Kliknij na pustej przestrzeni aby dodać węzeł.",
                    edgeDescription: "Kliknij na węzeł i przeciągnij linię aby powiązać go z innym węzłem.",
                    editEdgeDescription: "Kliknij na zakończeniu krawędzi i pociągnij, aby połączyć nią węzły.",
                    createEdgeError: "Nie można połączyć.",
                    deleteClusterError: "Nie można usunąć.",
                    editClusterError: "Nie można edytować."
                }
            };

            // create a network
            var container = document.getElementById("tree");
            var options = {
                locale: "pl",
                locales: locales,
                autoResize: true,
                height: "500px",
                width: "100%",
                layout: {
                    hierarchical: {
                        direction: "UD",
                    }
                },
                interaction: {
                    selectConnectedEdges: false,
                },
                manipulation: {
                    enabled: true,
                    addNode: function (nodeData, callback) {
                        var nodeName = $("#name").val();
                        if (nodeName) {
                            nodeData.label = nodeName;
                            callback(nodeData);
                            $("#name").val("");
                        } else {
                            alert("Uzupełnij nazwę węzła");
                        }
                    }
                },
            };
            TreePoligon.Tree = new vis.Network(container, dataSets, options);
        }

        draw();
    };

    var initTable = function() {
        $.ajax({
            type: "GET",
            url: "/api/graph",
            success: function (data) {
                if (data.success) {
                    data.data.forEach(function(element, index, array) {
                        $("#graphsTable>tbody").append("<tr>"+
                            "<th scope='row'>" + (index + 1) +"</th>"+
                            "<td>"+element.name+"</td>"+
                            "<td><div class='js-loadGraph btn btn-primary' data-graph-id='" + element.id +"'>Pokaż</div></td>"+
                            "</tr>");
                    });
                } else {
                    alert("Błąd podczas pobierania grafów!");
                }
            },
            failure: function (errMsg) {
                alert(errMsg);
            }
        }).done(function() {
            $(".js-loadGraph").on("click", function () {
                var graphId = $(this).data("graphId");
                initChart(graphId);
            });
        });
    };

    function toArray(obj) {
        var arr = [];
        Object.keys(obj).forEach(function (e) {
            arr.push(obj[e]);
        });

        return arr;
    }

    var init = function () {
        initTable();
        initEmptyChart();

        $("#btnSave").on("click", function () {
            var graphName = $("#graphName").val();
            if (graphName) {
                $.ajax({
                    type: "POST",
                    url: "/api/node",
                    data: JSON.stringify({
                        graphName: graphName,
                        nodes: toArray(TreePoligon.Tree.body.data.nodes._data),
                        edges: toArray(TreePoligon.Tree.body.data.edges._data)
                    }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(data) {
                        if (data.success) {
                            alert("Graf zapisany");
                        } else {
                            alert("Błąd podczas zapisu!");
                        }
                    },
                    failure: function(errMsg) {
                        alert(errMsg);
                    }
                });
            } else {
                alert("Uzupełnij nazwę grafu");
            }
        });

        $("#btnNew").on("click", function() {
            initEmptyChart();
        });
    };

    return {
        Init: init
    };
})();

TreePoligonService.Init();