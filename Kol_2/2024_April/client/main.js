import { ProjekcijaComponent } from "./components/projekcija_comp.js";
import { vratiProjekcije } from "./utils.js";

const projekcije = await vratiProjekcije();

projekcije.forEach(async (projekcija) => {
  const porjekcija_comp = new ProjekcijaComponent(projekcija);
  await porjekcija_comp.draw(document.body);
});
