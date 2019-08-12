import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { boardFailTypeActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.boardFailType.actionType,
    itemTypes.boardFailTypes.uri,
    actionTypes,
    config.appRoot
);
