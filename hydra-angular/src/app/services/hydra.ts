import { Injectable, signal } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class Hydra {

  private baseUrl = 'https://localhost:7100';

  token = signal<string | null>(null);
  briefing = signal<any>(null);
  analysis = signal<any>(null);
  loading = signal(false);
  error = signal<string | null>(null);

  constructor(private http: HttpClient) {}

  login(username: string, password: string) {

    this.loading.set(true);
    this.error.set(null);

    this.http.post<any>(
      `${this.baseUrl}/api/auth/login`,
      {
        username,
        password
      })
      .subscribe({
        next: response => {

          this.token.set(response.token);

          localStorage.setItem(
            'hydra_token',
            response.token);

          this.loading.set(false);
        },
        error: () => {

          this.error.set(
            'Login failed');

          this.loading.set(false);
        }
      });
  }

  getBriefing() {

    this.loading.set(true);
    this.error.set(null);

    const headers = this.getAuthHeaders();

    this.http.get<any>(
      `${this.baseUrl}/api/intelligence/briefing`,
      { headers })
      .subscribe({
        next: response => {

          this.briefing.set(response);

          this.loading.set(false);
        },
        error: () => {

          this.error.set(
            'Unable to retrieve briefing.');

          this.loading.set(false);
        }
      });
  }

  analyze(target: string, region: string) {

    this.loading.set(true);
    this.error.set(null);

    const headers = this.getAuthHeaders();

    this.http.post<any>(
      `${this.baseUrl}/api/intelligence/analyze`,
      {
        target,
        region
      },
      {
        headers
      })
      .subscribe({
        next: response => {

          this.analysis.set(response);

          this.loading.set(false);
        },
        error: () => {

          this.error.set(
            'Analysis failed.');

          this.loading.set(false);
        }
      });
  }

  private getAuthHeaders(): HttpHeaders {

    const savedToken =
      this.token() ??
      localStorage.getItem('hydra_token');

    return new HttpHeaders({
      Authorization: `Bearer ${savedToken}`
    });
  }
}
