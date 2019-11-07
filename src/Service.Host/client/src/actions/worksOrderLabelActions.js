import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { worksOrderLabelActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.worksOrderLabel.item,
    itemTypes.worksOrderLabel.actionType,
    itemTypes.worksOrderLabel.uri,
    actionTypes,
    config.appRoot
);
