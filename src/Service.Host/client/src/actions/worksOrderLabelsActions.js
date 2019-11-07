import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { worksOrderLabelsActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.worksOrderLabels.item,
    itemTypes.worksOrderLabels.actionType,
    itemTypes.worksOrderLabels.uri,
    actionTypes,
    config.appRoot
);
