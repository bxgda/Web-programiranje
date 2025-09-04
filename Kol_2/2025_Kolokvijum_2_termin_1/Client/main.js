import { BibliotekaComp } from "./components/biblioteka_comp.js";
import { vratiBiblioteke } from "./utils.js";

const biblioteke = await vratiBiblioteke();

biblioteke.forEach(async (biblioteka) => {
  const bibliotekaComp = new BibliotekaComp(biblioteka);
  await bibliotekaComp.draw(document.body);
});
