import { Form } from './components/Form.js';
import { Predmeti } from './components/Predmeti.js';
import { getRandomColor, preuzmiPredmete } from './utils.js';

const leviDiv = document.createElement('div');
leviDiv.classList.add('levi-div');

const desniDiv = document.createElement('div');
desniDiv.classList.add('desni-div');

export const cekiraniPredmeti = [];
export const studenti = [];

// Predmeti
const predmeti = await preuzmiPredmete();
predmeti.forEach((p) => {
  p.setBoja(getRandomColor());
});

const predmetiComp = new Predmeti(predmeti);
predmetiComp.draw(leviDiv);

// Studenti
const doleLevoDiv = document.createElement('div');
doleLevoDiv.classList.add('dole-levo-div');
leviDiv.append(doleLevoDiv);

// Forma desno
const form = new Form();
form.draw(desniDiv);

document.body.append(leviDiv, desniDiv);
