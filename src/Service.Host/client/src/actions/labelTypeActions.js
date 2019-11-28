import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { labelTypeActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.labelType.item,
    itemTypes.labelType.actionType,
    itemTypes.labelType.uri,
    actionTypes,
    config.appRoot
);
