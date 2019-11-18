import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { printWorksOrderLabelsActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.printWorksOrderLabels.actionType,
    actionTypes,
    defaultState,
    'Print Works Order label request submitted successfully'
);
