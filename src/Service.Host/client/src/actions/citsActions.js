import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { citsActionsTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.cits.actionType,
    itemTypes.cits.uri,
    actionTypes,
    config.appRoot
);
