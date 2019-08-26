import { getLoading, getCitsData, getInfoData } from '../productionMeasuresSelectors';

describe('when getting loading', () => {
    test('should set', () => {
        const state = {
            productionMeasures: {
                cits: { loading: true },
                info: { loading: true }
            }
        };

        expect(getLoading(state)).toEqual(true);
    });

    test('should set when only one', () => {
        const state = {
            productionMeasures: {
                cits: { loading: false },
                info: { loading: true }
            }
        };

        expect(getLoading(state)).toEqual(true);
    });
});

describe('when getting citsData', () => {
    test('should set', () => {
        const state = {
            productionMeasures: {
                cits: { loading: false, data: [{ citcode: 'A' }] }
            }
        };

        const expectedResult = [{ citcode: 'A' }];

        expect(getCitsData(state)).toEqual(expectedResult);
    });
});

describe('when getting infoData', () => {
    test('should get', () => {
        const state = {
            productionMeasures: {
                info: {
                    loading: false, data: { lastPtlJobref: 'AAAAAA'} }
            }
        };

        const expectedResult = { lastPtlJobref: 'AAAAAA' };

        expect(getInfoData(state)).toEqual(expectedResult);
    });
});