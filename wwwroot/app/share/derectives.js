angular
    .module("app")
    .directive("pagination", function () {
        return {
            scope: {
                current: "=",
                pages: "=",
                limit: "=",
            },
            template: `
        <div class="pt-3 d-flex align-items-center contents justify-content-end">
            <div class="pagnation">
                <span class="page-item" ng-repeat="x in  [].constructor(pages) track by $index"
                    ng-click="$parent.current = $index * limit; $parent.present = $index +1 "
                    >{{$index+1}}</span>
            </div>
            <p ng-init="present=1" class="mb-0 ml-5 italic bold">Trang {{present}} / {{pages}}</p>
        </div>
                    `,
        };
    })
    .directive("loading", function () {
        return {
            template: `
                    <div class="loader"><div></div><div></div><div></div><div></div>
                    </div>`,
        };
    });
