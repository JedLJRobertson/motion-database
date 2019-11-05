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
}
