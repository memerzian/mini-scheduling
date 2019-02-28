angular.module('SchedulingApp', [])
    .controller('BomController', function ($scope, $http) {
        $scope.data;

        $http.get("/api/GetBom/2").then(function (response) {
            $scope.data = response.data;

            root = $scope.data;

            update(root);

        });

        // ************** Generate the tree diagram	 *****************
        var margin = { top: 20, right: 120, bottom: 20, left: 120 },
            width = 960 - margin.right - margin.left,
            height = 500 - margin.top - margin.bottom;

        var i = 0;

        var tree = d3.layout.tree()
            .size([height, width]);

        var diagonal = d3.svg.diagonal();

        var svg = d3.select("body").append("svg")
            .attr("width", width + margin.right + margin.left)
            .attr("height", height + margin.top + margin.bottom)
            .call(d3.behavior.zoom().on("zoom", function () {
                svg.attr("transform", "translate(" + d3.event.translate + ")" + " scale(" + d3.event.scale + ")")
            }))
            .append("g")
            .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

        function update(source) {

            // Compute the new tree layout.
            var nodes = tree.nodes(root).reverse(),
                links = tree.links(nodes);

            // Normalize for fixed-depth.
            nodes.forEach(function (d) { d.y = d.depth * 180; });

            // Declare the nodes…
            var node = svg.selectAll("g.node")
                .data(nodes, function (d) { return d.id || (d.id = ++i); });

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

            // Declare the links…
            var link = svg.selectAll("path.link")
                .data(links, function (d) { return d.target.id; });

            // Enter the links.
            link.enter().insert("path", "g")
                .attr("d", diagonal)
                .attr("stroke", "black")
                .attr("stroke-width", 4)
                .attr("fill", "none");
        }
    });