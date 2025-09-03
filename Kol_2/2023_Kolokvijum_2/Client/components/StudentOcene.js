import { cekiraniPredmeti } from '../main.js';

export class StudentOcene {
  constructor(student) {
    this.student = student;
  }

  draw(container) {
    const box = document.createElement('div');
    box.classList.add('student', `student-${this.student.id}`);

    const naslov = document.createElement('h3');
    naslov.innerText = this.student.imePrezime;
    box.append(naslov);

    const stubiciDiv = document.createElement('div');
    stubiciDiv.className = 'stubici-div';

    cekiraniPredmeti.forEach((predmet) => {
      const stubic = document.createElement('div');
      stubic.style.backgroundColor = predmet.boja;
      stubic.style.width = '20px';

      const ocena = this.student.ocene.find((o) => o.idPredmeta == predmet.id);

      if (ocena) {
        stubic.style.height = `${ocena.vrednost * 10}%`;
      } else {
        stubic.style.height = '0%';
      }

      stubiciDiv.append(stubic);
    });

    box.append(stubiciDiv);

    container.append(box);
  }
}
