import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { printMACLabelsActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.printMACLabels.actionType,
    actionTypes,
    defaultState,
    'Print MAC label request submitted successfully'
);
