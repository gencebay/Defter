import * as React from "react";
import * as ReactDOM from "react-dom";
import { App, IAppDefinition } from "./App";

it("renders without crashing", () => {
  const appDefinition: IAppDefinition = {
    appTitle: "My App Title",
    examplePages: [],
    headerLinks: [],
    testPages: []
  };

  const div = document.createElement("div");
  ReactDOM.render(<App appDefinition={appDefinition} />, div);
});
