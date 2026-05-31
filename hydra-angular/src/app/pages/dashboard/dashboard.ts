import { JsonPipe } from '@angular/common';
import { Component, signal } from '@angular/core';
import { Hydra } from '../../services/hydra';

@Component({
  selector: 'app-dashboard',
  imports: [JsonPipe],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard {
  target = signal('Hydra Network');
  region = signal('Phoenix Sector');

  constructor(public hydra: Hydra) {}

  loadBriefing() {
    this.hydra.getBriefing();
  }

  runAnalysis() {
    this.hydra.analyze(
      this.target(),
      this.region()
    );
  }
}