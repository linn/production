import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { serialNumbersActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.serialNumbers.actionType,
    itemTypes.serialNumbers.uri,
    actionTypes,
    config.proxyRoot
);
