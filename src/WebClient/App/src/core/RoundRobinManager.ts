import { injectable, inject } from "inversify";
import { Settings } from "./Settings";
import { TypeConstants } from "../Constants";

@injectable()
export class RoundRobinManager {
  public settings: Settings;
  public readonly regionMap: Map<string, string[]>;
  public constructor(@inject(TypeConstants.Settings) settings: Settings) {
    this.settings = settings;

    const regions = this.settings.apiRegionKeys;
    this.regionMap = new Map();
    Object.keys(regions).forEach(key => {
      let values = (<string>regions[key]).split(",").map(s => s.trim());
      this.regionMap.set(key, values);
    });
  }

  public roundRobinUri = (regionKey: string): string => {
    const urls = this.regionMap.get(regionKey);
    if (urls !== undefined) {
      let url: string | undefined = urls.shift();
      if (url !== undefined) {
        urls.push(url);
        return url;
      }
    }

    throw Error("Url not found!");
    // tslint:disable-next-line:semicolon
  };

  public getAssetsUri = (): string => {
    const urls = this.regionMap.get("main");
    if (urls !== undefined) {
      let url: string = urls[0];
      return url;
    }

    throw Error("Url not found!");
    // tslint:disable-next-line:semicolon
  };
}
