import { Merac } from './merac.js';

export class Application {
  constructor(meraciFetched) {
    this.meraci = meraciFetched.map((merac) => {
      return new Merac(
        merac.id,
        merac.naziv,
        merac.interval,
        merac.boja,
        merac.granicaOd,
        merac.granicaDo,
        merac.trenutnaVrednost,
        merac.minimalnaIzmerenaVrednost,
        merac.maksimalnaIzmerenaVrednost,
        merac.prosecnaIzmerenaVrednost
      );
    });
  }

  draw(container) {
    this.meraci.forEach((merac) => {
      merac.draw(container);
    });
  }
}
