import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { partFailActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.partFail.item,
    itemTypes.partFail.actionType,
    itemTypes.partFail.uri,
    actionTypes,
    config.appRoot
);
