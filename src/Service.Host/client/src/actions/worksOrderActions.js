import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { worksOrderActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.worksOrder.item,
    itemTypes.worksOrder.actionType,
    itemTypes.worksOrder.uri,
    actionTypes,
    config.appRoot
);
