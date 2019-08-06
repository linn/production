import { getLoading, getCitsData } from '../productionMeasuresSelectors';

describe('when getting loading', () => {
    test('should set', () => {
        const state = {
            productionMeasures: {
                results: { loading: true }
            }
        };

        expect(getLoading(state)).toEqual(true);
    });
});

describe('when getting citsData', () => {
    test('should set', () => {
        const state = {
            productionMeasures: {
                results: { loading: false, data: [{ citcode: 'A' }] }
            }
        };

        const expectedResult = [{ citcode: 'A' }];

        expect(getCitsData(state)).toEqual(expectedResult);
    });
});
