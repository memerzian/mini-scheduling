angular.module('SchedulingApp', [])
    .controller('BomController', function ($scope, $http) {
        $scope.parts;

        $http.get("/api/GetAllParts").then(function (response) {
            $scope.parts = response.data;
        });

        $scope.loadBom = function(partID) {
            $http.get("/api/GetBom/" + partID).then(function (response) {
                root = response.data;

                update(root);
            });
        };


        // ************** Generate the tree diagram	 *****************
        var i = 0;

        var width = 600, height = 600;

        var tree = d3.layout.tree()
            .size([width, height]);

        var diagonal = d3.svg.diagonal();

        var svg = d3.select("div#container").append("svg")
            .attr("viewBox", " 0 -50 " + width + " " + height)
            .attr("preserveAspectRatio", "xMidYMid meet")
            .call(d3.behavior.zoom().on("zoom", function () {
                svg.attr("transform", "translate(" + d3.event.translate + ")" + " scale(" + d3.event.scale + ")")
            }))
            .append("g");

        function update(source) {
            // Clears the dom for loading new boms
            svg.selectAll("*").remove();

            // Compute the new tree layout.
            var nodes = tree.nodes(root).reverse(),
                links = tree.links(nodes);

            // Normalize for fixed-depth.
            nodes.forEach(function (d) { d.y = d.depth * 180; });

            // Declare the nodes…
            var node = svg.selectAll("g.node")
                .data(nodes, function (d) { return d.id || (d.id = ++i); });

            // Declare the links…
            var link = svg.selectAll("path.link")
                .data(links, function (d) { return d.target.id; });

            // Enter the links.
            var linkEnter = link.enter().append("g");

            linkEnter.append("path")
                .attr("d", diagonal)
                .attr("stroke", "black")
                .attr("stroke-width", 4)
                .attr("fill", "none");

            // Links have a source and a target node. So here, want to see the quantity of the target
            linkEnter.append("text")
                .text(function (d) { return d.target.Quantity; })
                .attr("transform", function (d) {
                    return "translate(" +
                        ((d.source.x + d.target.x) / 2) + "," +
                        ((d.source.y + d.target.y) / 2) + ")";
                })
                .attr("dx", "2em")
                .attr("text-anchor", "middle");

            // Enter the nodes.
            var nodeEnter = node.enter().append("g")
                .attr("class", "node")
                .attr("transform", function (d) {
                    return "translate(" + d.x + "," + d.y + ")";
                });

            nodeEnter.append("circle")
                .attr("r", 15)
                .attr("fill", "white")
                .attr("stroke-wtidth", 2)
                .attr("stroke", "black");

            nodeEnter.append("text")
                .attr("x", 35)
                .attr("dy", ".35em")
                .attr("text-anchor", "end")
                .text(function (d) { return d.Partnumber; })
                .style("fill-opacity", 1);
        }
    });