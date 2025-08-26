import { Draw } from "./draw.js";

let fabrike = [];

await fetch("http://localhost:5121/VratiFabrike").then(p => p.json()).then(
    factories => {
        factories.forEach(factory => {
            fabrike.push(factory);
        });

        let draw = new Draw(fabrike);
        draw.DrawApp(document.body);
    }
);