import { injectable } from "inversify";

// declare var __DEV__: boolean;

export interface Settings {
  app: string;
  project: string;
  apiRegionKeys: {
    main: string;
    authorization: string;
  };
}

@injectable()
export class DefaultSettings implements Settings {
  app: string;
  project: string;
  apiRegionKeys: {
    main: string;
    authorization: string;
  };

  constructor() {
    this.app = "My App";
    this.project = "My App (Development)";

    this.apiRegionKeys = {
      main: "http://httplive.netcorestack.com/",
      authorization: "http://httplive.netcorestack.com/"
    };
  }
}
