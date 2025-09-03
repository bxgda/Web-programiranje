import { StudentOcene } from './components/StudentOcene.js';
import { cekiraniPredmeti, studenti } from './main.js';
import { Ocena } from './models/Ocena.js';
import { Predmet } from './models/Predmet.js';
import { Student } from './models/Student.js';

const baseUrl = 'http://localhost:5192/Ocene';

export async function dodajOcenu(brojIndeksa, imePrezime, predmet, ocena) {
  const ocenaObj = {
    brojIndeksa,
    imePrezime,
    predmet,
    ocena,
  };

  try {
    const response = await fetch(`${baseUrl}/DodajOcenu`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(ocenaObj),
    });
    const data = await response.json();

    return data;
  } catch (error) {
    console.error(error);
    return null;
  }
}

export async function preuzmiPredmete() {
  try {
    const response = await fetch(`${baseUrl}/PreuzmiPredmete`);
    const data = await response.json();

    const predmeti = data.map((p) => new Predmet(p.id, p.naziv));

    return predmeti;
  } catch (error) {
    console.error(error);
    return [];
  }
}

export async function preuzmiOcene() {
  const predmetiIds = cekiraniPredmeti.map((p) => {
    return p.id;
  });

  try {
    const response = await fetch(`${baseUrl}/PreuzmiOcene`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(predmetiIds),
    });
    const data = await response.json();

    const studenti = [];
    data.forEach((ocena) => {
      const novaOcena = new Ocena(ocena.id, ocena.vrednost, ocena.predmet.id, ocena.predmet.naziv);

      const studentId = ocena.student.id;
      const vecDodat = studenti.find((s) => s.id === studentId);

      if (!vecDodat) {
        const noviStudent = new Student(
          ocena.student.id,
          ocena.student.indeks,
          ocena.student.imePrezime,
          [novaOcena]
        );

        studenti.push(noviStudent);
      } else {
        vecDodat.dodajOcenu(novaOcena);
      }
    });

    return studenti;
  } catch (error) {
    console.error(error);
    return [];
  }
}

export function drawStudenti() {
  const container = document.querySelector('.dole-levo-div');
  container.replaceChildren();

  studenti.forEach((student) => {
    const studentComp = new StudentOcene(student);
    studentComp.draw(container);
  });
}

export function dodajPredmetCheckbox(p, container, checked = false) {
  const checkBoxDiv = document.createElement('div');
  checkBoxDiv.className = 'checkbox-div';

  const checkBox = document.createElement('input');
  checkBox.type = 'checkbox';
  checkBox.id = p.id;
  checkBox.className = `checkbox-${p.id}`;
  checkBox.value = p.naziv;
  checkBox.checked = checked;
  checkBox.onchange = async () => {
    if (checkBox.checked) {
      if (!cekiraniPredmeti.some((item) => item.id === p.id)) {
        cekiraniPredmeti.push(p);
      }
    } else {
      const idx = cekiraniPredmeti.findIndex((item) => item.id === p.id);
      if (idx !== -1) cekiraniPredmeti.splice(idx, 1);
    }

    const noviStudenti = await preuzmiOcene();
    studenti.length = 0; // studenti su globalni state i mora prvo da ga obrisemo pa onda mu dodelimo nove studente
    studenti.push(...noviStudenti); // ... je spread operator (pogledaj na net)

    drawStudenti();
  };

  const lbl = document.createElement('label');
  lbl.htmlFor = p.id;
  lbl.textContent = p.naziv;
  lbl.style.color = p.boja;

  checkBoxDiv.append(checkBox, lbl);
  container.append(checkBoxDiv);
}

export function getRandomColor() {
  return '#' + Math.floor(Math.random() * 16777215).toString(16);
}
