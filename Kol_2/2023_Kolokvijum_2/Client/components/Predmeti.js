import { dodajPredmetCheckbox } from '../utils.js';

export class Predmeti {
  constructor(predmeti) {
    this.predmeti = predmeti;
  }

  draw(container) {
    const predmetiContainer = document.createElement('div');
    predmetiContainer.classList.add('predmeti-container');

    this.predmeti.forEach((p) => {
      dodajPredmetCheckbox(p, predmetiContainer);
    });

    container.append(predmetiContainer);
  }
}
