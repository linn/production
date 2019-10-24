import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { partFailErrorTypeActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.partFailErrorType.item,
    itemTypes.partFailErrorType.actionType,
    itemTypes.partFailErrorType.uri,
    actionTypes,
    config.appRoot
);
