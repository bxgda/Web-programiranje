import { Application } from "./application.js";
import { vratiFabrike } from "./utils.js";

let fabrike = await vratiFabrike();

fabrike.forEach(fab => {
    let app = new Application(fab);
    app.draw(document.body);
});