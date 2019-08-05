import { FetchApiActions } from '@linn-it/linn-form-components-library';
import { serialNumberReissueSalesArticleActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new FetchApiActions(
    itemTypes.serialNumberReissueSalesArticle.actionType,
    itemTypes.serialNumberReissueSalesArticle.uri,
    actionTypes,
    config.proxyRoot
);
