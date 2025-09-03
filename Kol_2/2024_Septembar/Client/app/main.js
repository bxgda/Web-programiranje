import { Application } from './applicaton.js';
import { fetchStanoviIds } from './utils.js';

const stanoviIds = await fetchStanoviIds();

const app = new Application(stanoviIds);
app.draw(document.body);
