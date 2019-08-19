import deepFreeze from 'deep-freeze';
import productionMeasures from '../productionMeasures';
import * as actionTypes from '../../actions';

describe('production measures reducer', () => {
    test('when requesting measures', () => {
        const state = {
            results: {
                loading: false,
                data: {}
            }
        };

        const action = {
            type: actionTypes.productionMeasuresCitsActionTypes.REQUEST_PRODUCTION_MEASURES_REPORT,
            payload: {}
        };

        const expected = {
            results: {
                loading: true,
                data: null
            }
        };

        deepFreeze(state);

        expect(productionMeasures(state, action)).toEqual(expected);
    });

    test('when receiving measures', () => {
        const state = {
            results: {
                loading: true,
                data: null
            }
        };

        const action = {
            type: actionTypes.productionMeasuresCitsActionTypes.RECEIVE_PRODUCTION_MEASURES_REPORT,
            payload: {
                data: [{ citcode: 'A' }]
            }
        };

        const expected = {
            results: {
                loading: false,
                data: [{ citcode: 'A' }]
            }
        };

        deepFreeze(state);

        expect(productionMeasures(state, action)).toEqual(expected);
    });
});
