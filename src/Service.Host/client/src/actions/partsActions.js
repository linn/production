import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { partsActionsTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.parts.actionType,
    itemTypes.parts.uri,
    actionTypes,
    config.appRoot
);
