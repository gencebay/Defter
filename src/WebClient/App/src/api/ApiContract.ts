export class ApiContract {
  regionKey: string;
  routeTemplate: string;
  constructor(regionKey: string, routeTemplate: string) {
    this.regionKey = regionKey;
    this.routeTemplate = routeTemplate;
  }

  getType = (): string => "ApiContract";
}
