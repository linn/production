import { processStoreFactory } from '@linn-it/linn-form-components-library';
import { buildSernosActionTypes as actionTypes } from '../../actions';
import * as processTypes from '../../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.buildSernos.actionType,
    actionTypes,
    defaultState,
    'Built Succesfully'
);
