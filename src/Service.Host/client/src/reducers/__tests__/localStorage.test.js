import deepFreeze from 'deep-freeze';
import localStorage from '../localStorage';
import * as actionTypes from '../../actions';
import * as itemTypes from '../../itemTypes';

describe('localStorage reducer', () => {
    describe('when setting works order default printer', () => {
        const state = [
            {
                item: itemTypes.worksOrder.item,
                defaultPrinter: ''
            }
        ];

        const action = {
            type: actionTypes.SET_DEFAULT_WORKS_ORDER_PRINTER,
            payload: {
                item: itemTypes.worksOrder.item,
                defaultPrinter: 'DSM'
            }
        };

        const expected = [
            {
                item: itemTypes.worksOrder.item,
                defaultPrinter: 'DSM'
            }
        ];

        it('should add the printer to state', () => {
            deepFreeze(state);

            expect(localStorage(state, action)).toEqual(expected);
        });
    });

    describe('when getting works order default printer', () => {
        const state = [
            {
                item: itemTypes.worksOrder.item,
                defaultPrinter: ''
            }
        ];

        const action = {
            type: actionTypes.SELECT_DEFAULT_WORKS_ORDER_PRINTER,
            payload: {
                item: itemTypes.worksOrder.item,
                defaultPrinter: 'DSM'
            }
        };

        const expected = [
            {
                item: itemTypes.worksOrder.item,
                defaultPrinter: 'DSM'
            }
        ];

        it('should add the printer to state', () => {
            deepFreeze(state);

            expect(localStorage(state, action)).toEqual(expected);
        });
    });
});
