import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { smtShiftsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.smtShifts.item,
    itemTypes.smtShifts.actionType,
    itemTypes.smtShifts.uri,
    actionTypes,
    config.appRoot
);
