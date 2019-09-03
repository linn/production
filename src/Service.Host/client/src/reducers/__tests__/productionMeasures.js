import deepFreeze from 'deep-freeze';
import productionMeasures from '../productionMeasures';
import * as actionTypes from '../../actions';

describe('production measures reducer', () => {
    test('when requesting cits', () => {
        const state = {
            cits: {
                loading: false,
                data: {}
            },
            info: {
                loading: false,
                data: null
            }
        };

        const action = {
            type:
                actionTypes.productionMeasuresCitsActionTypes
                    .REQUEST_PRODUCTION_MEASURES_CITS_REPORT,
            payload: {}
        };

        const expected = {
            cits: {
                loading: true,
                data: null
            },
            info: {
                loading: false,
                data: null
            }
        };

        deepFreeze(state);

        expect(productionMeasures(state, action)).toEqual(expected);
    });

    test('when receiving cits', () => {
        const state = {
            cits: {
                loading: true,
                data: null
            },
            info: {
                loading: false,
                data: null
            }
        };

        const action = {
            type:
                actionTypes.productionMeasuresCitsActionTypes
                    .RECEIVE_PRODUCTION_MEASURES_CITS_REPORT,
            payload: {
                data: [{ citcode: 'A' }]
            }
        };

        const expected = {
            cits: {
                loading: false,
                data: [{ citcode: 'A' }]
            },
            info: {
                loading: false,
                data: null
            }
        };

        deepFreeze(state);

        expect(productionMeasures(state, action)).toEqual(expected);
    });
});
