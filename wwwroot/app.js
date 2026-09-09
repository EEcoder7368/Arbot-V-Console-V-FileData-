const $ = (id) => document.getElementById(id);
const storageKey = 'arbotPagesState';
const dayNames = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
const emptyDay = ['Not set', 'Not set', 'Not set', 'Not set', 'Lunch', 'Not set', 'Not set'];

function initialState() {
  return {
    name: 'Jim Bob',
    positives: 0,
    negatives: 0,
    password: '',
    settings: { useName: true, darkMode: false },
    timetable: Object.fromEntries(dayNames.map((day) => [day.toLowerCase(), [...emptyDay]]))
  };
}

function loadState() {
  try {
    const saved = JSON.parse(localStorage.getItem(storageKey));
    return saved ? { ...initialState(), ...saved, settings: { ...initialState().settings, ...saved.settings }, timetable: { ...initialState().timetable, ...saved.timetable } } : initialState();
  } catch {
    return initialState();
  }
}

let state = loadState();
function saveState() { localStorage.setItem(storageKey, JSON.stringify(state)); }
function toast(message) { $('toast').textContent = message; $('toast').classList.remove('hidden'); setTimeout(() => $('toast').classList.add('hidden'), 2600); }

function localPeriod(time) {
  const minutes = time.getHours() * 60 + time.getMinutes();
  if (minutes >= 550 && minutes < 600) return 1;
  if (minutes >= 600 && minutes < 650) return 2;
  if (minutes >= 670 && minutes < 720) return 3;
  if (minutes >= 720 && minutes < 770) return 4;
  if (minutes >= 770 && minutes < 810) return 'Lunch';
  if (minutes >= 810 && minutes < 860) return 5;
  if (minutes >= 860 && minutes < 910) return 6;
  if (minutes >= 650 && minutes < 670) return 'Break';
  return 0;
}

function renderTable() {
  const today = new Date().toLocaleDateString('en-US', { weekday: 'long' });
  $('timetable').innerHTML = dayNames.map((day) => `<tr class="${day === today ? 'today' : ''}"><td>${day}</td>${state.timetable[day.toLowerCase()].map((lesson) => `<td>${lesson || '—'}</td>`).join('')}</tr>`).join('');
}

function render() {
  const now = new Date();
  const day = now.toLocaleDateString('en-US', { weekday: 'long' });
  const period = localPeriod(now);
  const lessons = state.timetable[day.toLowerCase()] || [];
  const currentLesson = typeof period === 'number' && period > 0 ? lessons[period - 1] : period || 'No lesson';
  $('login').classList.add('hidden');
  $('app').classList.remove('hidden');
  $('greeting').textContent = `Welcome, ${state.settings.useName ? state.name : 'back'}.`;
  $('positives').textContent = state.positives;
  $('negatives').textContent = state.negatives;
  $('day').textContent = day.toUpperCase();
  $('lesson').textContent = currentLesson;
  $('lessonMeta').textContent = typeof period === 'number' && period > 0 ? `Period ${period} is in session` : period === 'Break' ? 'Breaktime' : period === 'Lunch' ? 'Lunch' : 'No lessons are currently on';
  $('nameInput').value = state.name;
  $('useName').checked = state.settings.useName;
  $('darkMode').checked = state.settings.darkMode;
  document.body.classList.toggle('dark', state.settings.darkMode);
  renderTable();
}

function changeCounter(field, label) {
  const amount = Number(prompt(`How many ${label}?`, '1'));
  if (!Number.isInteger(amount) || amount <= 0) return;
  state[field] += amount;
  saveState();
  render();
  toast('Saved in this browser.');
}

document.querySelectorAll('.tab').forEach((button) => button.onclick = () => {
  document.querySelectorAll('.tab').forEach((tab) => tab.classList.remove('active'));
  document.querySelectorAll('.tab-panel').forEach((panel) => panel.classList.add('hidden'));
  button.classList.add('active');
  $(button.dataset.tab).classList.remove('hidden');
});

document.querySelector('[data-action="credits"]').onclick = () => changeCounter('positives', 'positives');
document.querySelector('[data-action="negatives"]').onclick = () => changeCounter('negatives', 'negatives');
$('saveLesson').onclick = () => {
  const lesson = $('editLesson').value.trim();
  if (!lesson) return;
  const day = $('editDay').value.toLowerCase();
  const period = Number($('editPeriod').value);
  state.timetable[day][period - 1] = lesson;
  saveState();
  $('editLesson').value = '';
  render();
  toast('Timetable updated in this browser.');
};
$('clearTimetable').onclick = () => {
  if (!confirm('Clear every saved lesson from Monday to Friday?')) return;
  dayNames.forEach((day) => { state.timetable[day.toLowerCase()] = [...emptyDay]; });
  saveState();
  render();
  toast('Timetable cleared.');
};
$('saveName').onclick = () => {
  state.name = $('nameInput').value.trim() || 'Student';
  saveState();
  render();
  toast('Name updated.');
};
$('saveSettings').onclick = () => {
  state.settings.useName = $('useName').checked;
  state.settings.darkMode = $('darkMode').checked;
  saveState();
  render();
  toast('Preferences saved.');
};
$('passwordForm').onsubmit = (event) => {
  event.preventDefault();
  state.password = $('newPassword').value;
  saveState();
  $('passwordNotice').textContent = 'Password saved in this browser.';
  $('passwordForm').reset();
};
$('logout').onclick = () => { state = initialState(); saveState(); location.reload(); };
$('loginForm').onsubmit = (event) => { event.preventDefault(); render(); };
$('showReset').onclick = () => $('resetForm').classList.toggle('hidden');
$('resetForm').onsubmit = (event) => { event.preventDefault(); $('resetNotice').textContent = 'This static site stores data only in your browser. Start fresh with Lock dashboard.'; };
setInterval(() => { $('clock').textContent = new Date().toLocaleString([], { weekday: 'short', month: 'short', day: 'numeric', hour: '2-digit', minute: '2-digit' }); }, 1000);
$('clock').textContent = '';
render();
