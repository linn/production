import getWorksOrderDefaultPrinter from '../localStorageSelectors';
import * as itemTypes from '../../itemTypes';

describe('when selecting works order default printer', () => {
    it('should return default printer', () => {
        const state = {
            localStorage: [
                {
                    item: itemTypes.worksOrder.item,
                    defaultPrinter: 'LP12'
                }
            ]
        };

        const expected = 'LP12';

        expect(getWorksOrderDefaultPrinter(state, itemTypes.worksOrder.item)).toEqual(expected);
    });
});
