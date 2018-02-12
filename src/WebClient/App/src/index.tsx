import * as React from "react";
import * as ReactDOM from "react-dom";
import { App, IAppDefinition } from "./App";
import registerServiceWorker from "./registerServiceWorker";
import "./index.css";

const appDefinition: IAppDefinition = {
  appTitle: "My App Title",
  examplePages: [],
  headerLinks: [],
  testPages: []
};

ReactDOM.render(<App appDefinition={appDefinition} />, document.getElementById(
  "root"
) as HTMLElement);
registerServiceWorker();
