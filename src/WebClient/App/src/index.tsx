import * as React from "react";
import { createApp } from "./utils/CreateApp";
import { examplesOf } from "./utils/Examples";
import { ActivityItemBasicExample } from "./components";

createApp(
  [
    examplesOf("Example Group")
      .add("Example 1", () => <div>Some content for Example 1</div>)
      .add("Example 2", () => <div>Some content for Example 2</div>)
      .add("Example 3", () => <div>Some content for Example 3</div>)
      .add("Example 4", () => (
        <div>
          <ActivityItemBasicExample />
        </div>
      ))
  ],
  () => <div>Example App Home</div>,
  "Example App"
);
