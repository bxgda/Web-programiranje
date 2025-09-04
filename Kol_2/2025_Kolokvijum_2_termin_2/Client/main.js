import { StovaristeCard } from "./components/StovaristeCard.js";
import { materijalUNajvecojKolicini, preuzmiStovarista } from "./utils.js";

const stovarista = await preuzmiStovarista();
const mainContainer = document.body;

stovarista.forEach(async (s) => {
  const stovariste = new StovaristeCard(s);
  stovariste.draw(mainContainer);
});
