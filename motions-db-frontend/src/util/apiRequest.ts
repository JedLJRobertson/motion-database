import * as rp from 'request-promise';
import { API_URL } from './config';

export default class ApiRequest {
  public static async Post(apiSubUrl: string, payload: any) {
    const options = {
      method: 'POST',
      uri: API_URL + apiSubUrl,
      body: payload,
      json: true,
    };

    return rp.default(options);
  }

  public static async Get(apiSubUrl: string) {
    const options = {
      method: 'GET',
      uri: API_URL + apiSubUrl,
      json: true,
    };

    return rp.default(options);
  }
}
