import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { componentCountsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.componentCounts.item,
    itemTypes.componentCounts.actionType,
    itemTypes.componentCounts.uri,
    actionTypes,
    config.appRoot
);
