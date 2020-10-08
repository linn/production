import { ProcessActions } from '@linn-it/linn-form-components-library';
import { buildSernosActionTypes as actionTypes } from './index';
import * as processTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    processTypes.buildSernos.item,
    processTypes.buildSernos.actionType,
    processTypes.buildSernos.uri,
    actionTypes,
    config.appRoot
);
