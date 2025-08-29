import { Application } from "./application.js";
import { vratiAutomobile } from "./utils.js";

let automobili = await vratiAutomobile();

let app = new Application(automobili);
app.draw(document.body);
